export async function GetFilmek() {
    const call = await fetch("https://localhost:7220/api/Film");
    const response = call.json();
    return response;
}

const BASE_URL = "https://localhost:7220/api";

export async function GetVetitesek() {
    const call = await fetch(`${BASE_URL}/Vetites`)
    const response = call.json();
    return response;
    
}
export async function GetVetitesekId(id) {
    const call = await fetch(`${BASE_URL}/Vetites/${id}`)
    const response = call.json();
    return response;
    
}
export async function GetSzekek() {
    const call = await fetch(`${BASE_URL}/Szek`)
    const response = call.json();
    return response;
    
}

// ── Auth ─────────────────────────────────────────────────────────────────────

export async function Register(nev, email, jelszo) {
    const call = await fetch(`${BASE_URL}/Auth/register`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ nev, email, jelszo })
    });
    const response = call.text();
    return response;
}

export async function Login(email, jelszo) {
    const call = await fetch(`${BASE_URL}/Auth/login`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, jelszo })
    });
    const response = call.json();
    return response;
}