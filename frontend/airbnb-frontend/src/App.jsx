import { useEffect, useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'

function App() {
  const [message, setMessage] = useState("");

  useEffect(()=>{
    fetch("https://localhost:7122/api/hello")
  .then((res)=> res.json())
.then((data) => setMessage(data.message))
.catch((err) => console.error(err));
},[]);
  return (
   <div>
    <h1>Airbnb Clone</h1>
    <p>{message}</p>
   </div>
  );
}

export default App
