import { useState } from "react";
import { AuthProvider, useAuth } from "../comps/Authcontext.jsx";
import LoginPage from "./Loginpage.jsx"
import RegisterPage from "./Registerpage.jsx";
import MyProfile from "./MyProfile.jsx";
import { refreshPage } from "../comps/Refresh.jsx";


function AppTartalom() {
    const { felhasznalo, kijelentkezes } = useAuth();
    const [oldal, setOldal] = useState("login"); // "login" | "register"


    if (!felhasznalo) {
        return oldal === "login"
            ? (
                <div className="profile-wrap">
                    <LoginPage />
                    <p className="no-account">
                        Nincs még fiókod?{" "}
                        <button onClick={() => setOldal("register")}>Regisztráció</button>
                    </p>
                </div>
            )
            : (
                <div className="profile-wrap">
                    <RegisterPage onRegisztralt={() => setOldal("login")} />
                </div>
            );
    }

    return (
        <div className="profile-wrap">
            <div className="profile-card">
                <MyProfile id={felhasznalo.id} />
                <button className="logout-btn" onClick={() => { kijelentkezes(); refreshPage(); }}>Kijelentkezés</button>
            </div>
        </div>
    );
}

export default function Profile() {
    return (
        <AuthProvider>
            <AppTartalom />
        </AuthProvider>
    );
}