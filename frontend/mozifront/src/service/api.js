const BASE_URL = "https://localhost:7220/api";

function getToken() {
    return localStorage.getItem("token");
}

// ==================== HELPER ====================
async function handleResponse(response) {
    if (!response.ok) {
        const errorText = await response.text();
        console.error(`API Error ${response.status}:`, errorText);
        throw new Error(`HTTP ${response.status} - ${errorText || response.statusText}`);
    }
    if (response.status === 204) return true; // No Content
    return response.json();
}

// ==================== GET ====================
export async function GetFilmek() {
    const res = await fetch(`${BASE_URL}/Film`);
    return handleResponse(res);
}
export async function GetFilmekId(id) {
    const res = await fetch(`${BASE_URL}/Film/${id}`);
    return handleResponse(res);
}
export async function GetTeremId(id) {
    const res = await fetch(`${BASE_URL}/Terem/${id}`);
    return handleResponse(res);
}

export async function GetVetitesek() {
    const res = await fetch(`${BASE_URL}/Vetites`);
    return handleResponse(res);
}

export async function GetVetitesekId(id) {
    const res = await fetch(`${BASE_URL}/Vetites/${id}`);
    return handleResponse(res);
}

export async function GetSzekek() {
    const res = await fetch(`${BASE_URL}/Szek`);
    return handleResponse(res);
}

export async function GetSzekekId(id) {
    const res = await fetch(`${BASE_URL}/Szek/${id}`);
    return handleResponse(res);
}

export async function GetFoglalas(id) {
    const token = getToken();
    const res = await fetch(`${BASE_URL}/Foglalas`, {
        headers: { Authorization: `Bearer ${token}` }
    });
    const data = await handleResponse(res);
    return data.filter(f => f.felhasznaloId === Number(id));
}

export async function GetFoglaltHelyek() {
    const call = await fetch(`${BASE_URL}/FoglaltHely`)
    const response = call.json();
    return response;

}


// ==================== DELETE ====================
export async function DeleteFoglalas(id) {
    try {
        const token = getToken();
        console.log("Deleting foglalas ID:", id, "with token:", token ? "exists" : "MISSING!");

        const res = await fetch(`${BASE_URL}/Foglalas/${id}`, {
            method: "DELETE",
            headers: { 
                "Authorization": `Bearer ${token}`
            }
        });

        return handleResponse(res); // will throw if not ok
    } catch (error) {
        console.error("DeleteFoglalas failed:", error.message);
        throw error;
    }
}

// ==================== POST ====================
export async function CreateFoglalas(felhasznaloId) {
    const res = await fetch(`${BASE_URL}/Foglalas`, {
        method: "POST",
        headers: { 
            "Content-Type": "application/json",
            "Authorization": `Bearer ${getToken()}`
        },
        body: JSON.stringify({ felhasznaloId })
    });
    return handleResponse(res);
}

export async function CreateFoglaltHely(szekId, foglalasId, vetitesId) {
    const res = await fetch(`${BASE_URL}/FoglaltHely`, {
        method: "POST",
        headers: { 
            "Content-Type": "application/json",
            "Authorization": `Bearer ${getToken()}`
        },
        body: JSON.stringify({ szekId, foglalasId, vetitesId })
    });
    return handleResponse(res);
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