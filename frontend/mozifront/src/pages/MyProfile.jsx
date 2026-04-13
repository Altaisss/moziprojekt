import { useState, useEffect } from "react";
import { GetFoglalas, GetSzekekId, GetVetitesekId, DeleteFoglalas, GetFilmekId, GetTeremId } from "../service/api";

function MyProfile({ id }) {
    const [myfoglalas, setMyfoglalas] = useState([]);

    function formatDatum(datum) {
        return datum.replace("T", " ").slice(0, 16).replace(/-/g, ".");
    }

    useEffect(() => {
        async function betolt(id) {
            try {
                const foglalasok = await GetFoglalas(id);

                const enriched = await Promise.all(
                    foglalasok.map(async (f) => {
                        const helyek = await Promise.all(
                            f.foglalthely.map(async (hely) => {
                                const [szek, vetites] = await Promise.all([
                                    GetSzekekId(hely.szekId),
                                    GetVetitesekId(hely.vetitesId)
                                ]);

                                const film = vetites?.filmId ? await GetFilmekId(vetites.filmId) : null;
                                const terem = vetites?.teremId ? await GetTeremId(vetites.teremId) : null;

                                return { ...hely, szek, vetites, film, terem };
                            })
                        );

                        return { ...f, foglalthely: helyek };
                    })
                );

                setMyfoglalas(enriched);
            } catch (error) {
                console.error("Hiba a foglalások betöltésekor:", error);
            }
        }

        betolt(id);
    }, [id]);

    async function handleDelete(foglalasId) {
        if (!window.confirm("Biztosan törölni szeretnéd ezt a foglalást?")) return;

        try {
            const success = await DeleteFoglalas(foglalasId);
            if (success) {
                setMyfoglalas(prev => prev.filter(f => f.id !== foglalasId));
            }
        } catch (error) {
            alert("Törlés sikertelen.");
            console.error(error);
        }
    }

    return (
        <>
            <h2>Foglalásaim</h2>
            {myfoglalas.length === 0 ? (
                <p>Nincs foglalásod.</p>
            ) : (
                myfoglalas.map(f => {
                    const firstHely = f.foglalthely[0]; 

                    return (
                        <div key={f.id} style={{ marginBottom: "25px", padding: "15px", border: "1px solid #ccc", borderRadius: "8px" }}>

                            
                            {firstHely && (
                                <>
                                    <p><strong>Film:</strong> {firstHely.film?.cim || firstHely.vetites?.filmNev}</p>
                                    <p><strong>Terem:</strong> {firstHely.terem?.teremNev || "Ismeretlen"}</p>
                                    <p><strong>Időpont:</strong> {formatDatum(firstHely.vetites?.idopont)}</p>
                                </>
                            )}

                            
                            <p><strong>Foglalt helyek:</strong></p>
                            <ul>
                                {f.foglalthely.map(hely => (
                                    <li key={hely.id}>
                                        {hely.szek?.sor}. sor, {hely.szek?.szam}. hely
                                    </li>
                                ))}
                            </ul>

                            <button 
                                onClick={() => handleDelete(f.id)}
                                style={{ marginTop: "10px", color: "red" }}
                            >
                                Foglalás törlése
                            </button>
                        </div>
                    );
                })
            )}
        </>
    );
}

export default MyProfile;