import FilmGrid from "../comps/FilmGrid.jsx";
import "../css/Home.css"

function Home({ filmek }) {
   return (
        <>
            <FilmGrid filmek={filmek} />
            <div className="welcome-section">
                <h1 className="welcome-title">Fedezd fel a világ filmjeit</h1>
                <p className="welcome-text">
                    Mozink célja, hogy bemutassa a különböző országok és régiók gazdag filmes kultúráját. 
                    Hisszük, hogy egy film akkor élhető át igazán, ha az alkotók eredeti nyelvén szólal meg — 
                    ezért vetítéseinken a filmek kizárólag eredeti nyelven, magyar felirattal láthatók.
                </p>
            </div>
        </>
    );
}

export default Home;