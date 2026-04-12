import { useState } from "react";
import { useAuth } from "../comps/Authcontext.jsx";
import { refreshPage } from "../comps/Refresh.jsx";
import '../css/Login.css'

export default function RegisterPage({ onRegisztralt }) {
    const { regisztracio } = useAuth();

    const [nev, setNev] = useState("");
    const [email, setEmail] = useState("");
    const [jelszo, setJelszo] = useState("");
    const [jelszoMegint, setJelszoMegint] = useState("");
    const [hiba, setHiba] = useState(null);
    const [siker, setSiker] = useState(false);
    const [tolt, setTolt] = useState(false);

    async function handleSubmit(e) {
        e.preventDefault();
        setHiba(null);

        if (jelszo !== jelszoMegint) {
            setHiba("A két jelszó nem egyezik.");
            return;
        }

        setTolt(true);
        const result = await regisztracio(nev, email, jelszo);
        setTolt(false);

        if (result.success) {
            setSiker(true);
        } else {
            setHiba(result.hiba);
        }
    }

    if (siker) {
        return (
            <div className="siker-wrap">
                <p style={{ color: "green" }}>Sikeres regisztráció! 🎉</p>
                <button onClick={() => { refreshPage(); onRegisztralt(); }}>Bejelentkezés</button>
            </div>
        );
    }

    return (
        <form className="register-form" onSubmit={handleSubmit}>
            <h2>Regisztráció</h2>

            <input
                type="text"
                placeholder="Név"
                value={nev}
                onChange={e => setNev(e.target.value)}
                required
            />

            <input
                type="email"
                placeholder="Email"
                value={email}
                onChange={e => setEmail(e.target.value)}
                required
            />

            <input
                type="password"
                placeholder="Jelszó"
                value={jelszo}
                onChange={e => setJelszo(e.target.value)}
                required
            />

            <input
                type="password"
                placeholder="Jelszó mégegyszer"
                value={jelszoMegint}
                onChange={e => setJelszoMegint(e.target.value)}
                required
            />

            {hiba && <p style={{ color: "red" }}>{hiba}</p>}

            <button type="submit" disabled={tolt}>
                {tolt ? "Betöltés..." : "Regisztráció"}
            </button>

            <p>
                Már van fiókod?{" "}
                <button onClick={() => { refreshPage(); onRegisztralt(); }}>Bejelentkezés</button>
            </p>
        </form>
    );
}