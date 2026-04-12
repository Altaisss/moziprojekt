// components/AdminRoute.jsx
import { Navigate } from "react-router-dom";
import { isAdmin } from "../comps/Authcontext.jsx";

function AdminRoute({ children }) {
    if (!isAdmin()) {
        return <Navigate to="/" replace />;
    }
    return children;
}

export default AdminRoute;