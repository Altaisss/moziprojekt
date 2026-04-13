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
        setKivalasztottSzekek([]); 
    };

    return (
        <div className="szek-terkep">
            <div className="screen">VÁSZON</div>

            <div className="mozi">
                {sorok.map(sor => {
                    const balSzekek = szekek
                        .filter(s => s.sor === sor && s.oldal === 'B')
                        .sort((a, b) => a.szam - b.szam);

                    const jobbSzekek = szekek
                        .filter(s => s.sor === sor && s.oldal === 'J')
                        .sort((a, b) => a.szam - b.szam);

                    return (
                        <div className="sor" key={sor}>
                            <div className="sor-szam">{sor}</div>

                            <div className="oldal bal">
                                {balSzekek.map(szek => (
                                    <button
                                        key={szek.id}
                                        onClick={() => szekKattintas(szek.id)}
                                        className={getSzekClass(szek)}
                                        disabled={foglaltSzekek.includes(szek.id)}
                                    >
                                        {szek.szam}
                                    </button>
                                ))}
                            </div>

                            <div className="folyoso"></div>

                            <div className="oldal jobb">
                                {jobbSzekek.map(szek => (
                                    <button
                                        key={szek.id}
                                        onClick={() => szekKattintas(szek.id)}
                                        className={getSzekClass(szek)}
                                        disabled={foglaltSzekek.includes(szek.id)}
                                    >
                                        {szek.szam}
                                    </button>
                                ))}
                            </div>

                            <div className="sor-szam">{sor}</div>
                        </div>
                    );
                })}
            </div>

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