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

function App() {

  const { felhasznalo } = useAuth();
  const [filmek, setFilmek] = useState([]);

  useEffect(() => {
    async function Filmekbetolt() {
      const be = await GetFilmek()
      setFilmek(be);

    }
    Filmekbetolt();
  }, [])

  const [vetitesek, setVetitesek] = useState();
  useEffect(() => {
      async function Vetitesekbetolt() {
          const be = await GetVetitesek()
          setVetitesek(be);

      }
      Vetitesekbetolt();
  }, [])
  console.log(filmek);

  return (
    <>
      <NavBar />
      <Routes>
        <Route path='/' element={<Home filmek={filmek} />} />
        <Route path='/profile' element={<Profile />} />
        <Route path='/vetitesek' element={<Vetitesek filmek={filmek} vetitesek={vetitesek} />} />
        <Route path='/vetitesek/:id' element={<Foglalas/>}/>
      </Routes>
    </>
  )
}

export default App
