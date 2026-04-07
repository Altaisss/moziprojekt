import { Link } from "react-router-dom"
import { useAuth } from "./Authcontext";
import { useState } from "react";
import { jwtDecode } from 'jwt-decode'
import "../css/NavBar.css"


export function NavBar() {

    return (
        <nav className="nav-bar">
            <div className="nav-links">
                <Link to='/'>Home</Link>
                <Link to='/vetitesek'>Vetitesek</Link>
            </div>
            <div className="nav-profile nav-links">
                <Link to='/profile'>Profile</Link>
            </div>
        </nav>
    )


}