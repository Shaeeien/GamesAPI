import React, { useCallback, useState, useContext } from "react";
import '../layout/index.css'
import axios from 'axios';
import {useNavigate} from 'react-router-dom';
import App from "../App";
import UserContext from './userContext';

export default function LoginForm(){
    const navigate = useNavigate();
    const[email, setEmail] = useState("");
    const[password, setPassword] = useState(""); 
    var context = useContext(UserContext);       
      
    async function TryLoggingIn(email, password){

        const headers = {
            'Content-Type': 'application/json;charset=UTF-8',
            'Accept' : '*/*',
            'Access-Control-Allow-Origin': '*'
          }
        const userData = {
            "Email" : email, 
            "Password" : password      
        }
    
        console.log('loggin...');
        const response = await axios.post("https://localhost:7278/api/user/login", JSON.stringify(userData), { 
                headers: headers 
            }).then((response) => {                
                console.log(response.status)     
                if(response.status === 200){
                    UserContext.email = email.toString();
                    //Do rozkminy, jak to ustawić?
                    console.log(UserContext.email);
                    navigate("/");
                }       
                else{
                    //Tutaj <p></p> z tekstem o błędnych danych logowania
                }
            });         
    }
    

    return <div id="loginform">
        <form>
            <label className="formLabel" htmlFor="email">Adres e-mail:</label>
            <input className="formInput" type="text" onChange={(e) => setEmail(e.target.value)}/>
            <br />
            <label className="formLabel" htmlFor="password">Hasło:</label>
            <input className="formInput" type="password" onChange={(e) => setPassword(e.target.value)}/>
            <br />
            <input value="Zaloguj" type="button" onClick={() => TryLoggingIn(email, password)} />
        </form>
        </div>
    
}

