import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import CustomHeading3 from './CustomHeading3'

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
      <CustomHeading3 />
    </>
  )
}

export default App
