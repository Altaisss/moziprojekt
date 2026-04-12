// pages/AdminFilmek.jsx
import { useEffect, useState } from "react";
import { GetFilmek, DeleteFilm, CreateFilm, UpdateFilm } from "../service/api.js";
import '../css/AdminFilm.css'
import { refreshPage } from "./Refresh.jsx";

const EMPTY_FORM = { cim: "", rendezo: "", hossz: "", leiras: "", kep: null };

function AdminFilmek() {
    const [filmek, setFilmek] = useState([]);
    const [form, setForm] = useState(EMPTY_FORM);
    const [editId, setEditId] = useState(null);
    const [preview, setPreview] = useState(null);
    const [error, setError] = useState("");
    const [success, setSuccess] = useState("");

    useEffect(() => { loadFilmek(); }, []);

    async function loadFilmek() {
        try {
            const data = await GetFilmek();
            setFilmek(data);
        } catch (e) {
            setError("Nem sikerült betölteni a filmeket.");
        }
    }

    function handleChange(e) {
        const { name, value, files } = e.target;
        if (name === "kep") {
            setForm(f => ({ ...f, kep: files[0] }));
            setPreview(URL.createObjectURL(files[0]));
        } else {
            setForm(f => ({ ...f, [name]: value }));
        }
    }

    function startEdit(film) {
        setEditId(film.id);
        setForm({
            cim: film.cim,
            rendezo: film.rendezo,
            hossz: film.hossz,
            leiras: film.leiras || "",
            kep: null
        });
        setPreview(`https://localhost:7220${film.kepUrl}`);
        setError("");
        setSuccess("");
        window.scrollTo({ top: 0, behavior: "smooth" });
    }

    function cancelEdit() {
        setEditId(null);
        setForm(EMPTY_FORM);
        setPreview(null);
        setError("");
    }

    async function handleSubmit(e) {
        e.preventDefault();
        setError("");
        setSuccess("");

        const formData = new FormData();
        formData.append("cim", form.cim);
        formData.append("rendezo", form.rendezo);
        formData.append("hossz", form.hossz);
        formData.append("leiras", form.leiras);
        if (form.kep) formData.append("kep", form.kep);

        try {
            if (editId) {
                await UpdateFilm(editId, formData);
                setSuccess("Film sikeresen frissítve.");
            } else {
                await CreateFilm(formData);
                setSuccess("Film sikeresen hozzáadva.");
            }
            cancelEdit();
            loadFilmek();
            refreshPage();
        } catch (e) {
            setError("Hiba történt a mentés során.");
        }
    }

    async function handleDelete(id) {
        if (!window.confirm("Biztosan törölni szeretnéd ezt a filmet?")) return;
        try {
            await DeleteFilm(id);
            setSuccess("Film törölve.");
            loadFilmek();
            refreshPage();
        } catch (e) {
            setError("Hiba történt a törlés során.");
        }
    }

    return (
        <div className="admin-wrap">
            <h1>Filmek kezelése</h1>

            {error && <div className="admin-msg error">{error}</div>}
            {success && <div className="admin-msg success">{success}</div>}

            <form className="admin-form" onSubmit={handleSubmit}>
                <h2>{editId ? "Film szerkesztése" : "Új film hozzáadása"}</h2>

                <div className="form-row">
                    <div className="form-group">
                        <label>Cím *</label>
                        <input name="cim" value={form.cim} onChange={handleChange} required />
                    </div>
                    <div className="form-group">
                        <label>Rendező *</label>
                        <input name="rendezo" value={form.rendezo} onChange={handleChange} required />
                    </div>
                </div>

                <div className="form-row">
                    <div className="form-group">
                        <label>Hossz (perc) *</label>
                        <input name="hossz" type="number" min="1" max="500" value={form.hossz} onChange={handleChange} required />
                    </div>
                    <div className="form-group">
                        <label>Plakát</label>
                        <input name="kep" type="file" accept="image/*" onChange={handleChange} />
                    </div>
                </div>

                <div className="form-group">
                    <label>Leírás</label>
                    <textarea name="leiras" value={form.leiras} onChange={handleChange} rows={4} />
                </div>

                {preview && (
                    <img src={preview} alt="preview" className="poster-preview" />
                )}

                <div className="form-actions">
                    <button type="submit" className="btn-primary">
                        {editId ? "Mentés" : "Hozzáadás"}
                    </button>
                    {editId && (
                        <button type="button" className="btn-secondary" onClick={cancelEdit}>
                            Mégse
                        </button>
                    )}
                </div>
            </form>

            <div className="admin-table-wrap">
                <table className="admin-table">
                    <thead>
                        <tr>
                            <th>Plakát</th>
                            <th>Cím</th>
                            <th>Rendező</th>
                            <th>Hossz</th>
                            <th>Műveletek</th>
                        </tr>
                    </thead>
                    <tbody>
                        {filmek.map(film => (
                            <tr key={film.id} className={editId === film.id ? "editing" : ""}>
                                <td>
                                    {film.kepUrl && (
                                        <img
                                            src={`https://localhost:7220${film.kepUrl}`}
                                            alt={film.cim}
                                            className="table-poster"
                                        />
                                    )}
                                </td>
                                <td>{film.cim}</td>
                                <td>{film.rendezo}</td>
                                <td>{film.hossz} perc</td>
                                <td>
                                    <button className="btn-edit" onClick={() => startEdit(film)}>Szerkesztés</button>
                                    <button className="btn-delete" onClick={() => handleDelete(film.id)}>Törlés</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
}

export default AdminFilmek;