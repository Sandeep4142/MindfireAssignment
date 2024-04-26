import React, {useState, useEffect} from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import './App.css';
import SignIn from './Components/SignInSignUp/SignIn';
import SignUp from './Components/SignInSignUp/SignUp';
import Patient from './Components/Patient/Patient';
import Header from './Components/Header';
import Appointment from './Components/Appointment/Appointment';

function App() {
  const [isSignedIn, setIsSignedIn] = useState(true)

  useEffect(() =>{
    if(localStorage.getItem('user')){
      console.log(localStorage.getItem('user'))
      setIsSignedIn(true);
    }
  }
  )
  return (
    <>
      <Header isSignedIn={isSignedIn} setIsSignedIn={setIsSignedIn} />
      <br />
      <div className='components'>
      <Routes>
        <Route path="/" element={isSignedIn?<Patient /> : <SignIn isSignedIn={isSignedIn} setIsSignedIn={setIsSignedIn}/>} />
        <Route path="/SignIn"  element={<SignIn isSignedIn={isSignedIn} setIsSignedIn={setIsSignedIn} />} />
        <Route path="/SignUp" element={<SignUp isSignedIn={isSignedIn} setIsSignedIn={setIsSignedIn} />} />
        <Route path="/Patient" element={<Patient isSignedIn={isSignedIn} />} />
        <Route path="/Appointment" element={<Appointment isSignedIn={isSignedIn} />} />
      </Routes>
      </div>
    </>
  );
}

export default App;
