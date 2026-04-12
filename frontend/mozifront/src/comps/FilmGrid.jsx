import '../css/FilmGrid.css'
import { Link } from 'react-router-dom';

function FilmGrid({ filmek = [] }) {
  return (
    <div className="film-grid-wrap">
      <h2>Műsoron:</h2>
      <div className="film-grid">
        {filmek.map((film, i) => (
          <Link to={`/filmek/${film.id}`}  className="poster-card" key={i}>
            <div className="poster-img-wrap">
              <img
                src={`https://localhost:7220${film.kepUrl}`}
                alt={film.cim}
                onError={(e) => { e.target.src = "/placeholder-poster.jpg"; }}
              />
            </div>
            <div className="poster-label">{film.cim}</div>
          </Link>
        ))}
      </div>
    </div>
  );
}

export default FilmGrid;