import { GetVetitesek } from "../service/api";
import { useState, useEffect } from "react";
import Table from "../comps/Table";
function Vetitesek({filmek, vetitesek}) {


    try {
       // console.log(vetitesek);

       // console.log(vetitesek[0].idopont.split("T")[1].slice(0, -3))
       // console.log(vetitesek[0].idopont.split("T")[0].slice(5).replace('-', '.'))
        var idopontok = [];
        vetitesek.map((vet) => {
            if (!idopontok.includes(vet.idopont.split("T")[0].slice(5).replace('-', '.'))) {
                idopontok.push(vet.idopont.split("T")[0].slice(5).replace('-', '.'))
            }
        })
       // console.log(idopontok)

        

        return (
            <>
                <Table vetitesek={vetitesek} idopontok={idopontok} filmek={filmek}/>
            </>
        )

    } catch (e) {
        console.log(e.message);
    }
}
export default Vetitesek;
