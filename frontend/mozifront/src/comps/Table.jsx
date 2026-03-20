import { Link } from "react-router-dom";

function Table({ vetitesek, idopontok, filmek }) {
    try {
        return (
            <table>
                <thead>
                    <tr>
                        <th></th> 
                        {idopontok.map((ido) => (
                            <th key={ido}>{ido}</th>
                        ))}
                    </tr>
                </thead>
                <tbody>
                    {filmek.map((film) => (
                        <tr key={film.id}>
                            <td>{film.cim}</td>
                            {idopontok.map((ido) => (
                                <td key={ido}>
                                    {vetitesek
                                        .filter(vet =>
                                            vet.filmId === film.id &&
                                            vet.idopont.split("T")[0].slice(5).replace('-', '.') === ido
                                        )
                                        .map(vet => (
                                            <div key={vet.id}>
                                               <Link to={`/vetitesek/${vet.id}`}> {vet.idopont.split("T")[1].slice(0, -3)}</Link>
                                            </div>
                                        ))
                                    }
                                </td>
                            ))}
                        </tr>
                    ))}
                </tbody>
            </table>
        );
    } catch (e) {
        console.log(e.message)
    }
}
export default Table;