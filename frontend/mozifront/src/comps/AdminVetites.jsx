// pages/AdminVetitesek.jsx
import { useEffect, useState } from "react";
import { GetVetitesek, GetFilmek, GetTeremek, DeleteVetites, CreateVetites, UpdateVetites } from "../service/api.js";
import '../css/AdminFilm.css'
import { refreshPage } from "./Refresh.jsx";


const EMPTY_FORM = {
    filmId: "",
    teremId: "",
    idopont: "",
    jegyAr: "",
    nyelv: "",
};

function AdminVetitesek() {
    const [vetitesek, setVetitesek] = useState([]);
    const [filmek, setFilmek] = useState([]);
    const [termek, setTermek] = useState([]);
    const [form, setForm] = useState(EMPTY_FORM);
    const [editId, setEditId] = useState(null);
    const [error, setError] = useState("");
    const [success, setSuccess] = useState("");

    useEffect(() => { loadAll(); }, []);

    async function loadAll() {
        try {
            const [v, f, t] = await Promise.all([GetVetitesek(), GetFilmek(), GetTeremek()]);
            setVetitesek(v);
            setFilmek(f);
            setTermek(t);
        } catch (e) {
            setError("Nem sikerült betölteni az adatokat.");
        }
    }

    function checkConflict(payload) {
    const incoming = new Date(payload.idopont);
    const incomingFilm = filmek.find(f => f.id === payload.filmId);
    const incomingHossz = incomingFilm?.hossz ?? 120;
    const incomingEnd = new Date(incoming.getTime() + incomingHossz * 60000);

    return vetitesek.find(v => {
        if (v.teremId !== payload.teremId) return false;
        if (editId && v.id === editId) return false;

        const existing = new Date(v.idopont);
        if (existing.toDateString() !== incoming.toDateString()) return false; // different day

        const existingFilm = filmek.find(f => f.id === v.filmId);
        const existingHossz = existingFilm?.hossz ?? 120;
        const existingEnd = new Date(existing.getTime() + existingHossz * 60000);

        return incoming < existingEnd && incomingEnd > existing; // proper overlap check
    });
}

    function handleChange(e) {
        const { name, value } = e.target;
        setForm(f => ({ ...f, [name]: value }));
    }

    function startEdit(v) {
        setEditId(v.id);
        setForm({
            filmId: v.filmId,
            teremId: v.teremId,
            idopont: v.idopont,
            jegyAr: v.jegyAr,
            nyelv: v.nyelv || ""
        });
        setError("");
        setSuccess("");
        window.scrollTo({ top: 0, behavior: "smooth" });
    }

    function cancelEdit() {
        setEditId(null);
        setForm(EMPTY_FORM);
        setError("");
    }

    async function handleSubmit(e) {
        e.preventDefault();
        setError("");
        setSuccess("");

        const payload = {
            filmId: Number(form.filmId),
            teremId: Number(form.teremId),
            idopont: form.idopont + ":00",
            jegyAr: Number(form.jegyAr),
            nyelv: form.nyelv
        };

        const conflict = checkConflict(payload);
        if (conflict) {
            const teremNev = getTeremNev(payload.teremId);
            const conflictTime = new Date(conflict.idopont).toLocaleString("hu-HU");
            const conflictFilm = getFilmCim(conflict.filmId);
            alert(`Ütközés! A ${teremNev} teremben ${conflictTime}-kor már fut: "${conflictFilm}".`);
            return;
        }

        try {
            if (editId) {
                await UpdateVetites(editId, payload);
                setSuccess("Vetítés sikeresen frissítve.");
            } else {
                await CreateVetites(payload);
                setSuccess("Vetítés sikeresen hozzáadva.");
            }
            cancelEdit();
            loadAll();
            refreshPage();
        } catch (e) {
            setError("Hiba történt a mentés során.");
        }
    }

    async function handleDelete(id) {
        if (!window.confirm("Biztosan törölni szeretnéd ezt a vetítést?")) return;
        try {
            await DeleteVetites(id);
            setSuccess("Vetítés törölve.");
            loadAll();
            refreshPage();
        } catch (e) {
            setError("Hiba történt a törlés során.");
        }
    }

    function getFilmCim(id) {
        return filmek.find(f => f.id === id)?.cim || id;
    }

    function getTeremNev(id) {
        return termek.find(t => t.id === id)?.teremNev || id;
    }

    return (
        <div className="admin-wrap">
            <h1>Vetítések kezelése</h1>

            {error && <div className="admin-msg error">{error}</div>}
            {success && <div className="admin-msg success">{success}</div>}

            <form className="admin-form" onSubmit={handleSubmit}>
                <h2>{editId ? "Vetítés szerkesztése" : "Új vetítés hozzáadása"}</h2>

                <div className="form-row">
                    <div className="form-group">
                        <label>Film *</label>
                        <select name="filmId" value={form.filmId} onChange={handleChange} required>
                            <option value="">Válassz filmet...</option>
                            {filmek.map(f => (
                                <option key={f.id} value={f.id}>{f.cim}</option>
                            ))}
                        </select>
                    </div>
                    <div className="form-group">
                        <label>Terem *</label>
                        <select name="teremId" value={form.teremId} onChange={handleChange} required>
                            <option value="">Válassz termet...</option>
                            {termek.map(t => (
                                <option key={t.id} value={t.id}>{t.teremNev}</option>
                            ))}
                        </select>
                    </div>
                </div>

                <div className="form-row">
                    <div className="form-group">
                        <label>Időpont *</label>
                        <input name="idopont" type="datetime-local" value={form.idopont} onChange={handleChange} required />
                    </div>
                    <div className="form-group">
                        <label>Jegyár (Ft) *</label>
                        <input name="jegyAr" type="number" min="1" value={form.jegyAr} onChange={handleChange} required />
                    </div>
                </div>

                <div className="form-row">
                    <div className="form-group">
                        <label>Nyelv</label>
                        <input name="nyelv" value={form.nyelv} onChange={handleChange} placeholder="pl. Magyar, Japán, Orosz" />
                    </div>
                </div>

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
                            <th>Film</th>
                            <th>Terem</th>
                            <th>Időpont</th>
                            <th>Jegyár</th>
                            <th>Nyelv</th>
                            <th>Műveletek</th>
                        </tr>
                    </thead>
                    <tbody>
                        {vetitesek.map(v => (
                            <tr key={v.id} className={editId === v.id ? "editing" : ""}>
                                <td>{getFilmCim(v.filmId)}</td>
                                <td>{getTeremNev(v.teremId)}</td>
                                <td>{new Date(v.idopont).toLocaleString("hu-HU")}</td>
                                <td>{v.jegyAr} Ft</td>
                                <td>{v.nyelv || "—"}</td>
                                <td>
                                    <button className="btn-edit" onClick={() => startEdit(v)}>Szerkesztés</button>
                                    <button className="btn-delete" onClick={() => handleDelete(v.id)}>Törlés</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
}

export default AdminVetitesek;