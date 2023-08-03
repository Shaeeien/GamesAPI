import React, { useState } from "react";
import '../layout/index.css'
import axios from 'axios';
import {useNavigate} from 'react-router-dom';


export default function LoginForm(props){
    const navigate = useNavigate();
    const[email, setEmail] = useState("");
    const[password, setPassword] = useState("");       
    const [showPassword, setShowPassword] = useState(false);

    async function TryLoggingIn(email, password){

        const headers = {
            'Content-Type': 'application/json;charset=UTF-8',
            'Accept' : '*/*',
            'Access-Control-Allow-Origin': '*'
          }
        const userData = {
            email : email, 
            password : password      
        }
    
        console.log('loggin...');
        const response = await axios.post("https://localhost:7054/api/auth/login", JSON.stringify(userData), {
            headers: headers
        });
        console.log(response);
        if (response.status === 200)
        {                         
            localStorage.setItem("email", email);
            props.Login(true);
            navigate("/");            
        }       
        else{
            //Tutaj <p></p> z tekstem o błędnych danych logowania
        }
                     
    }

    const TogglePassword = function () {
        setShowPassword(!showPassword);
        document.querySelector("#toggle-password").classList.toggle('fa-eye-slash');
    }


    return <div id="loginform">
        <form>
            <label className="formLabel" htmlFor="email">Adres e-mail:</label>
            <input className="formInput" type="text" onChange={(e) => setEmail(e.target.value)}/>
            <br />
            <label className="formLabel" htmlFor="password">Hasło:</label>
            <input className="formInput" type={showPassword ? "text" : "password"} onChange={(e) => setPassword(e.target.value)} />
            <i class="far fa-eye" id="toggle-password" onClick={TogglePassword}></i>
            <br />
            <input value="Zaloguj" type="button" onClick={() => TryLoggingIn(email, password)} />
        </form>
        </div>
    
}

