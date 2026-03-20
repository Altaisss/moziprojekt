import '../css/SzekTerkep.css'

function SzekTerkep({ szekek, foglaltSzekek = [] }) {
    const sorok = [...new Set(szekek.map(s => s.sor))].sort((a, b) => a - b);

    return (
        <div className="szek-terkep">
            <div className="screen">🎬 VÁSZON</div>

            <table>
                <tbody>
                    {sorok.map(sor => {
                        const balSzekek = szekek.filter(s => s.sor === sor && s.oldal === 'B').sort((a, b) => a.szam - b.szam);
                        const jobbSzekek = szekek.filter(s => s.sor === sor && s.oldal === 'J').sort((a, b) => a.szam - b.szam);

                        return (
                            <tr key={sor}>
                                <td className="sor-szam">{sor}</td>

                                {balSzekek.map(szek => (
                                    <td key={szek.id}>
                                        <button
                                            className={`szek ${foglaltSzekek.includes(szek.id) ? 'foglalt' : 'szabad'}`}
                                            disabled={foglaltSzekek.includes(szek.id)}
                                        >
                                            {szek.szam}
                                        </button>
                                    </td>
                                ))}

                                <td className="folyoso"></td>  {/* folyosó */}

                                {jobbSzekek.map(szek => (
                                    <td key={szek.id}>
                                        <button
                                            className={`szek ${foglaltSzekek.includes(szek.id) ? 'foglalt' : 'szabad'}`}
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

            <div className="jelmagyarazat">
                <span className="szek szabad">🟢 Szabad</span>
                <span className="szek foglalt">🔴 Foglalt</span>
            </div>
        </div>
    );
}
export default SzekTerkep;