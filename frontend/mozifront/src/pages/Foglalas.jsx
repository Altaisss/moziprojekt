import { useParams } from "react-router-dom";
import { GetVetitesekId, GetSzekek } from "../service/api";
import { useState, useEffect } from "react";
import SzekTerkep from "../comps/SzekTerkep";

function Foglalas() {
    try {

        const [vetites, setVetites] = useState()
        const [szekek, setSzekek] = useState()
        const { id } = useParams();
        useEffect(() => {
            async function Vetitesbetolt() {
                const be = await GetVetitesekId(id)
                setVetites(be);

            }
            Vetitesbetolt();
        }, [])
        useEffect(() => {
            async function SzekekBetolt() {
                const be = await GetSzekek()
                setSzekek(be);

            }
            SzekekBetolt();
        }, [])
        console.log(vetites)
        console.log(szekek)
        const teremszekek = szekek.filter(szek => szek.teremId === vetites.teremId)
        console.log(teremszekek)




        return (
            <>
                <SzekTerkep szekek={teremszekek} foglaltszekek={[]} />
            </>
        )

    } catch (e) {
        console.log(e);
    }
}
export default Foglalas;