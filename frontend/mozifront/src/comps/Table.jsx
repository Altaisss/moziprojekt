import { Link } from "react-router-dom";
import "../css/Table.css";

function Table({ vetitesek, idopontok, filmek }) {
    const sortedIdopontok = [...idopontok].sort((a, b) => {
        const [monthA, dayA] = a.split('.').map(Number);
        const [monthB, dayB] = b.split('.').map(Number);
        return monthA - monthB || dayA - dayB;
    });

    return (
        <div className="showtime-table">
            <div className="showtime-row showtime-header"
                style={{
                    gridTemplateColumns: `380px repeat(${sortedIdopontok.length}, 1fr)`
                }}>
                <div className="showtime-cell">Film</div>
                {sortedIdopontok.map((ido) => (
                    <div key={ido} className="showtime-cell">{ido}</div>
                ))}
            </div>

            {filmek.map((film) => (
                <div key={film.id} className="showtime-row"
                    style={{
                        gridTemplateColumns: `380px repeat(${sortedIdopontok.length}, 1fr)`
                    }}>

                    <Link className="showtime-cell film-title" to={`/filmek/${film.id}`} key={film.id}>
                        <img
                            className="film-poster"
                            src={`https://localhost:7220${film.kepUrl}`}
                            alt={film.cim}
                        />
                        <strong>{film.cim}</strong>
                    </Link>


                    {sortedIdopontok.map((ido) => {
                        const filteredVetites = vetitesek
                            .filter(vet =>
                                vet.filmId === film.id &&
                                vet.idopont.split("T")[0].slice(5).replace('-', '.') === ido
                            )
                            .sort((a, b) =>
                                a.idopont.split("T")[1].localeCompare(b.idopont.split("T")[1])
                            );

                        return (
                            <div key={ido} className="showtime-cell time-cell">
                                {filteredVetites.map(vet => {
                                    const time = vet.idopont.split("T")[1].slice(0, -3);
                                    return (
                                        <div key={vet.id} className="time-slot">
                                            <Link to={`/vetitesek/${vet.id}`}>
                                                {time}
                                            </Link>
                                        </div>
                                    );
                                })}
                            </div>
                        );
                    })}
                </div>
            ))}
        </div>
    );
}

export default Table;