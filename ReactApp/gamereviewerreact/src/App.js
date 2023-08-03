import React from "react";
import { Navbar } from './layout/navbar';
import { Footer } from './layout/footer';
import { Route, Routes } from 'react-router-dom';
import Register from "./user/register";
import Login from "./user/login";
import AddGame from "./game/addgame";
import AddProducer from "./producer/addproducer";
import AddingFailed from "./producer/addingfailed";
import AddingSuccess from "./producer/addingsuccess";
import { Home } from "./pages/home";
import GamesList from "./game/gameslist";
import ProtectedRoute from "./routes/protectedroute";
import { useState } from 'react';

export default function App() {

    const[loggedIn, setLoggedIn] = useState(false);

    const Logout = function(){
        console.log("test");
        localStorage.removeItem("email");
        setLoggedIn(false);
    }

    return <div className="App">
        <Navbar loggedIn={loggedIn} Logout={Logout} Login={setLoggedIn} />      

        <Routes>
            <Route path="/" element={<Home />} />
            <Route element={<ProtectedRoute /> }>
                <Route path="/games" element={<GamesList />} />
            </Route>
                     
            <Route path="/register" element={<Register />} />
            <Route path="/login" element={<Login Login={setLoggedIn} />} />
            <Route path="/games/add" element={<AddGame />} />
            <Route path="/producers/add" element={<AddProducer />} />
            <Route path="/producers/add/success" element={<AddingSuccess />} />
            <Route path="/producers/add/failure" element={<AddingFailed />} />

        </Routes>

       <Footer></Footer>                         
    </div>
}
    