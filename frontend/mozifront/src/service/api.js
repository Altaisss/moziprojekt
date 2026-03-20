export async function GetFilmek() {
    const call = await fetch("https://localhost:7220/api/Film");
    const response = call.json();
    return response;
}