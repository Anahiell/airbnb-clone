import { useEffect, useState } from 'react'
import './App.css'

function App() {
  const [message, setMessage] = useState("");

  useEffect(()=>{
    fetch("http://localhost:8080/GetHello")
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
