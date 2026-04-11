import { Link } from "react-router-dom";
import "../css/Table.css";

function TableOne({ vetitesek, idopontok }) {

    const sortedIdopontok = [...idopontok].sort((a, b) => {
        const [monthA, dayA] = a.split('.').map(Number);
        const [monthB, dayB] = b.split('.').map(Number);
        return monthA - monthB || dayA - dayB;
    });

    return (
        <div className="showtime-table">

            {/* HEADER */}
            <div
                className="showtime-row showtime-header"
                style={{
                    gridTemplateColumns: `repeat(${sortedIdopontok.length}, 1fr)`
                }}
            >
                {sortedIdopontok.map((ido) => (
                    <div key={ido} className="showtime-cell">{ido}</div>
                ))}
            </div>

            {/* ROW (only one film's times) */}
            <div
                className="showtime-row"
                style={{
                    gridTemplateColumns: `repeat(${sortedIdopontok.length}, 1fr)`
                }}
            >
                {sortedIdopontok.map((ido) => {
                    const filteredVetites = vetitesek
                        .filter(vet =>
                            vet.idopont.split("T")[0].slice(5).replace('-', '.') === ido
                        )
                        .sort((a, b) =>
                            a.idopont.split("T")[1].localeCompare(b.idopont.split("T")[1])
                        );

                    return (
                        <div key={ido} className="showtime-cell time-cell">
                            {filteredVetites.length === 0 && (
                                <span className="no-show">—</span>
                            )}

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
        </div>
    );
}

export default TableOne;