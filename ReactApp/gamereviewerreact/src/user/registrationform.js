import React from "react";
import '../layout/index.css';
import { useState } from 'react';

export default function RegistrationForm(){
    const [showPassword, setShowPassword] = useState(false);
    const [showRepeatPassword, setShowRepeatPassword] = useState(false);
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [repeatPassword, setRepeatPassword] = useState("");


    const TogglePassword = function () {
        setShowPassword(!showPassword);
        document.querySelector("#toggle-password").classList.toggle('fa-eye-slash');
    }

    const ToggleRepeatPassword = function () {
        setShowRepeatPassword(!showRepeatPassword);
        document.querySelector("#toggle-repeat-password").classList.toggle('fa-eye-slash');
    }

    const RegisterUser = async function (){
        const headers = {
            'Content-Type': 'application/json;charset=UTF-8',
            'Accept' : '*/*'
          }
        const userData = {
            "Email" : email, 
            "Password" : password, 
            "ConfirmPassword" : repeatPassword, 
            "RoleId" : 0        
        }
        console.log(userData);
        console.log("register");
        const response = await fetch('https://localhost:7278/api/user/register', {
         method: 'POST',
         body: JSON.stringify(userData), // string or object
         headers: headers
    }).then(response => {
        response.json().then((data) => {
            console.log(data);
          })
        }
      );
    }

    return <div id="registerform">
                <form>
                    <label className="formLabel" htmlFor="email">Adres e-mail : </label>
                    <input className="formInput" type="text" onChange={(e) => setEmail(e.target.value)}/>
                    <br />
                    <label className="formLabel" htmlFor="password">Hasło : </label>
                    <input className="formInput" type={showPassword ? "text" : "password"} onChange={(e) => setPassword(e.target.value)} />
                    <i class="far fa-eye" id="toggle-password" onClick={TogglePassword}></i>
                    <br />
                    <label className="formLabel" htmlFor="repeatPassword">Powtórz hasło : </label>
                    <input className="formInput" type={showRepeatPassword ? "text" : "password"} onChange={(e) => setRepeatPassword(e.target.value)} />
                    <i class="far fa-eye" id="toggle-repeat-password" onClick={ToggleRepeatPassword}></i>
                    <br />
                    <input type="button" value="Zarejestruj" onClick={() => RegisterUser()} />
                </form>
            </div>
    
    
}
