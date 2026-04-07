import { useState } from "react";
import { AuthProvider, useAuth } from "../comps/Authcontext.jsx";
import LoginPage from "./Loginpage.jsx"
import RegisterPage from "./Registerpage.jsx";
import MyProfile from "./MyProfile.jsx";

function AppTartalom() {
    const { felhasznalo, kijelentkezes } = useAuth();
    const [oldal, setOldal] = useState("login"); // "login" | "register"


    if (!felhasznalo) {
        return oldal === "login"
            ? (
                <>
                    <LoginPage />
                    <p>
                        Nincs még fiókod?{" "}
                        <button onClick={() => setOldal("register")}>Regisztráció</button>
                    </p>
                </>
            )
            : (

                    <RegisterPage onRegisztralt={() => setOldal("login")} />
            );
    }

    return (
        <>
            <MyProfile id={felhasznalo.id} />
            <button onClick={kijelentkezes}>Kijelentkezés</button>
        </>
    );
}

export default function Profile() {
    return (
        <AuthProvider>
            <AppTartalom />
        </AuthProvider>
    );
}