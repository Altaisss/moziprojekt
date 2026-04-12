import { Link } from "react-router-dom"
import { useAuth } from "./Authcontext";
import { isAdmin } from "./Authcontext";
import "../css/NavBar.css"

export function NavBar() {
    const { felhasznalo } = useAuth();

    return (
        <nav className="nav-bar">
            <div className="nav-links">
                <Link to='/'>Home</Link>
                <Link to='/vetitesek'>Vetitesek</Link>
                {felhasznalo && isAdmin() && <Link to='/admin'>Admin</Link>}
            </div>
            <div className="nav-profile nav-links">
                <Link to='/profile'>Profile</Link>
            </div>
        </nav>
    )
}