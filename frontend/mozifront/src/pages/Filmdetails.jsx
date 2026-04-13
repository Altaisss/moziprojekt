import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { GetFilmekId, GetVetitesek } from "../service/api";
import TableOne from "../comps/TableOne";
import "../css/Filmdetails.css"

function Filmdetails() {
    const { id } = useParams();

    const [film, setFilm] = useState(null);
    const [vetitesek, setVetitesek] = useState([]);

    useEffect(() => {
        async function betolt() {
            const f = await GetFilmekId(id);
            const v = await GetVetitesek();

            setFilm(f);
            setVetitesek(v);
        }
        betolt();
    }, [id]);

    const filtered = vetitesek.filter(v => v.filmId === film?.id);

    const idopontok = [
        ...new Set(
            filtered.map(v =>
                v.idopont.split("T")[0].slice(5).replace("-", ".")
            )
        )
    ];

    if (!film) return <div>Loading...</div>;

    console.log(film)

    return (
        <div className="seating-page">
            <div className="movie-details">
                <img
                    className="movie-poster-large"
                    src={`https://localhost:7220${film.kepUrl}`}
                    alt={film.cim}
                />

                <div className="movie-info">
                    <h1>{film.cim}</h1>

                    {film.leiras && <p>{film.leiras}</p>}
                    {film.hossz && <span> {film.hossz} min</span>}
                    {film.rendezo && <span> {film.rendezo}</span>}
                </div>
            </div>

            <TableOne
                vetitesek={filtered}
                idopontok={idopontok}
                filmek={[film]}
            />
        </div>
    );
}

export default Filmdetails;