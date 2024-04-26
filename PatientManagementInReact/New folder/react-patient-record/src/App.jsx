import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import Patient from './Components/Patient'

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
      <Patient />
    </>
  )
}

export default App
