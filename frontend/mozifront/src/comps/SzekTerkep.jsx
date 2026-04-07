import { useState } from 'react';
import '../css/SzekTerkep.css';

function SzekTerkep({ szekek, foglaltSzekek, onFoglalas }) {
    const [kivalasztottSzekek, setKivalasztottSzekek] = useState([]);

    const sorok = [...new Set(szekek.map(s => s.sor))].sort((a, b) => a - b);

    const szekKattintas = (szekId) => {
        setKivalasztottSzekek(prev =>
            prev.includes(szekId)
                ? prev.filter(id => id !== szekId)
                : [...prev, szekId]
        );
    };

    const getSzekClass = (szek) => {
        if (foglaltSzekek.includes(szek.id)) return 'szek foglalt';
        if (kivalasztottSzekek.includes(szek.id)) return 'szek kijelolt';
        return 'szek szabad';
    };

    const handleFoglalas = () => {
        if (onFoglalas) onFoglalas(kivalasztottSzekek);
        setKivalasztottSzekek([]); // reset kijelölés
    };

    return (
        <div className="szek-terkep">
            <div className="screen">🎬 VÁSZON</div>

            <table>
                <tbody>
                    {sorok.map(sor => {
                        const balSzekek = szekek
                            .filter(s => s.sor === sor && s.oldal === 'B')
                            .sort((a, b) => a.szam - b.szam);
                        const jobbSzekek = szekek
                            .filter(s => s.sor === sor && s.oldal === 'J')
                            .sort((a, b) => a.szam - b.szam);

                        return (
                            <tr key={sor}>
                                <td className="sor-szam">{sor}</td>
                                {balSzekek.map(szek => (
                                    <td key={szek.id}>
                                        <button
                                            onClick={() => szekKattintas(szek.id)}
                                            className={getSzekClass(szek)}
                                            disabled={foglaltSzekek.includes(szek.id)}
                                        >
                                            {szek.szam}
                                        </button>
                                    </td>
                                ))}
                                <td className="folyoso"></td>
                                {jobbSzekek.map(szek => (
                                    <td key={szek.id}>
                                        <button
                                            onClick={() => szekKattintas(szek.id)}
                                            className={getSzekClass(szek)}
                                            disabled={foglaltSzekek.includes(szek.id)}
                                        >
                                            {szek.szam}
                                        </button>
                                    </td>
                                ))}
                                <td className="sor-szam">{sor}</td>
                            </tr>
                        );
                    })}
                </tbody>
            </table>

            <div className="foglalas-panel">
                <p>{kivalasztottSzekek.length} szék kijelölve</p>
                <button
                    onClick={handleFoglalas}
                    disabled={kivalasztottSzekek.length === 0}
                    className="foglalas-gomb"
                >
                    Foglalás
                </button>
            </div>
        </div>
    );
}

export default SzekTerkep;