import { useEffect, useState } from 'react'
import { GetFilmek, GetVetitesek } from './service/api';
import { NavBar } from './comps/NavBar';
import Home from './pages/Home';
import { Routes, Route } from 'react-router-dom';
import Profile from './pages/Profile';
import { useAuth } from './comps/Authcontext';
import "./css/App.css"
import Vetitesek from './pages/Vetitesek';
import Foglalas from './pages/Foglalas';
import Filmdetails from './pages/Filmdetails';
import AdminRoute from './comps/Adminroute';
import AdminPage from './pages/AdminPage';

function App() {

  const { felhasznalo } = useAuth();
  const [filmek, setFilmek] = useState([]);
  const [vetitesek, setVetitesek] = useState();

 useEffect(() => {
  async function betolt() {
    const [filmekData, vetitesekData] = await Promise.all([
      GetFilmek(),
      GetVetitesek()
    ]);

    setFilmek(filmekData);
    setVetitesek(vetitesekData);
  }

  betolt();
}, []);
  console.log(filmek);

  return (
    <>
      <NavBar />
      <Routes>
        <Route path='/' element={<Home filmek={filmek} />} />
        <Route path='/profile' element={<Profile />} />
        <Route path='/vetitesek' element={<Vetitesek filmek={filmek} vetitesek={vetitesek} />} />
        <Route path='/vetitesek/:id' element={<Foglalas/>}/>
        <Route path='/filmek/:id' element={<Filmdetails/>}/>
        <Route path="/admin"element={
        <AdminRoute>
            <AdminPage />
        </AdminRoute>
    }
/>
      </Routes>
    </>
  )
}

export default App
