import { createContext, useContext, useState } from "react";
import { jwtDecode } from "jwt-decode";
import { Login, Register } from "../service/api";

const AuthContext = createContext();

function tokenToFelhasznalo(token) {
    const decoded = jwtDecode(token);
    return {
        token,
        id: decoded.sub,
        email: decoded.email,
        nev: decoded.nev
    };
}

export function AuthProvider({ children }) {
    const [felhasznalo, setFelhasznalo] = useState(() => {
        const token = localStorage.getItem("token");
        return token ? tokenToFelhasznalo(token) : null;
    });

    async function bejelentkezes(email, jelszo) {
        const result = await Login(email, jelszo);
        if (result.token) {
            localStorage.setItem("token", result.token);
            setFelhasznalo(tokenToFelhasznalo(result.token));
           // console.log("Felhasználó beállítva:", tokenToFelhasznalo(result.token));
            return { success: true };
        }
        return { success: false, hiba: "Hibás email vagy jelszó." };
    }

    async function regisztracio(nev, email, jelszo) {
        const result = await Register(nev, email, jelszo);
        if (typeof result === "string" && result.includes("Sikeres")) {
            return { success: true };
        }
        return { success: false, hiba: result?.title ?? result ?? "Sikertelen regisztráció." };
    }

    function kijelentkezes() {
        localStorage.removeItem("token");
        setFelhasznalo(null);
    }

    return (
        <AuthContext.Provider value={{ felhasznalo, bejelentkezes, regisztracio, kijelentkezes }}>
            {children}
        </AuthContext.Provider>
    );
}

export function useAuth() {
    return useContext(AuthContext);
}