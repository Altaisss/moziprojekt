import { Link } from "react-router-dom"
import { useAuth } from "./Authcontext";
import { useState } from "react";
import { jwtDecode } from 'jwt-decode'
import "../css/NavBar.css"


export function NavBar() {

    //const felhasznalo = jwtDecode(localStorage.getItem("token")) || "Profile";
    const { felhasznalo } = useAuth();
    console.log(felhasznalo)

    return (
        <nav className="nav-bar">
            <div className="nav-links">
                <Link to='/'>Home</Link>
                <Link to='/vetitesek'>Vetitesek</Link>
            </div>
            <div className="nav-profile nav-links">
                <Link to='/profile'>{felhasznalo ? felhasznalo.nev : "Profile"}</Link>
            </div>
        </nav>
    )


}