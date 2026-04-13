import { useParams } from "react-router-dom";
import { GetVetitesekId, GetSzekek, GetFoglaltHelyek, CreateFoglalas, CreateFoglaltHely } from "../service/api";
import { useState, useEffect } from "react";
import { useAuth } from "../comps/Authcontext";
import SzekTerkep from "../comps/SzekTerkep";

function Foglalas() {
    const { id } = useParams();
    const { felhasznalo } = useAuth();

    const [vetites, setVetites] = useState(null);
    const [szekek, setSzekek] = useState([]);
    const [foglaltSzekek, setFoglaltSzekek] = useState([]);

    useEffect(() => {
        async function betolt() {
            const v = await GetVetitesekId(id);
            const s = await GetSzekek();
            const fh = await GetFoglaltHelyek();

           
            const foglaltIds = fh
                .filter(f => f.vetitesId === Number(id))
                .map(f => f.szekId);

            setVetites(v);
            setSzekek(s);
            setFoglaltSzekek(foglaltIds);
        }
        betolt();
    }, [id]);

    if (!vetites || szekek.length === 0) return <p>Betöltés...</p>;

    const teremSzekek = szekek.filter(szek => szek.teremId === vetites.teremId);

    const handleFoglalas = async (kivalasztottSzekek) => {
        const foglalas = await CreateFoglalas(Number(felhasznalo.id));

        for (const szekId of kivalasztottSzekek) {
            await CreateFoglaltHely(szekId, foglalas.id, Number(id));
        }

        
        setFoglaltSzekek(prev => [...prev, ...kivalasztottSzekek]);
        alert(`Sikeres foglalás! ${kivalasztottSzekek.length} szék lefoglalva.`);
    };

    return (
        <SzekTerkep
            szekek={teremSzekek}
            foglaltSzekek={foglaltSzekek}
            onFoglalas={handleFoglalas}
        />
    );
}

export default Foglalas;