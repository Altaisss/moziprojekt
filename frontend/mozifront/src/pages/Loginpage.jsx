import { useState } from "react";
import { useAuth } from "../comps/Authcontext.jsx";
import { useNavigate } from "react-router-dom";
import { refreshPage } from "../comps/Refresh.jsx";
import '../css/Login.css'

export default function LoginPage() {
    const { bejelentkezes } = useAuth();
    const navigate = useNavigate();

    const [email, setEmail] = useState("");
    const [jelszo, setJelszo] = useState("");
    const [hiba, setHiba] = useState(null);
    const [tolt, setTolt] = useState(false);

    async function handleSubmit(e) {
        e.preventDefault();
        setHiba(null);
        setTolt(true);

        const result = await bejelentkezes(email, jelszo);

         if (result.success) {
            navigate("/"); 
            refreshPage();
        } else {
            setHiba(result.hiba);
            setEmail("") ;
            setJelszo("");
        }
      
        setTolt(false);
    }

    return (
        <form className="login-form" onSubmit={handleSubmit}>
            <h2>Bejelentkezés</h2>

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

            {hiba && <p className="hiba" style={{ color: "red" }}>{hiba}</p>}

            <button type="submit" disabled={tolt}>
                {tolt ? "Betöltés..." : "Bejelentkezés"}
            </button>
        </form>
    );
}