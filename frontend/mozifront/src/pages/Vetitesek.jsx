import Table from "../comps/Table";
import '../css/Vetites.css';

function Vetitesek({ filmek = [], vetitesek = [] }) {

    const idopontok = [
        ...new Set(
            vetitesek.map(v =>
                v.idopont.split("T")[0].slice(5).replace("-", ".")
            )
        )
    ];

    if (!vetitesek.length) {
        return <div className="screening-page">No showtimes available</div>;
    }

    return (
        <div className="screening-page">
            <Table
                vetitesek={vetitesek}
                idopontok={idopontok}
                filmek={filmek}
            />
        </div>
    );
}

export default Vetitesek;