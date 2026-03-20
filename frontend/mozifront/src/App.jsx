import { useEffect, useState } from 'react'
import { GetFilmek } from './service/api';

function App() {

  const [filmek, setFilmek] = useState([]);

  useEffect(() => {
    async function Filmekbetolt() {
      const be = await GetFilmek()
      setFilmek(be);

    }
    Filmekbetolt();
  }, [])
  console.log(filmek);

  return (
    <>
    </>
  )
}

export default App
