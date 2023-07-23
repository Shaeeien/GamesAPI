import React from "react";
import { useState } from "react";
import axios from "axios";
import '../layout/index.css';

export default class RegistrationForm extends React.Component{
    constructor(props){
        super(props);
        this.state = {
            email : "",
            password : "",
            repeatPassword : ""
        }        
    }
    RegisterUser = async () =>{
        const headers = {
            'Content-Type': 'application/json;charset=UTF-8',
            'Accept' : '*/*'
          }
        const userData = {
            "Email" : this.email, 
            "Password" : this.password, 
            "ConfirmPassword" : this.repeatPassword, 
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

    setEmail = (value) => {
        this.email = value;
        console.log(this.email);
    }

    setPassword = (value) => {
        this.password = value;
    }

    setRepeatPassword = (value) => {
        this.repeatPassword = value;
    }

    render(){
        return <div id="registerform">
        <form>
            <label className="formLabel" htmlFor="email">Adres e-mail : </label>
            <input className="formInput" type="text" onChange={(e) => this.setEmail(e.target.value)}/>
            <br />
            <label className="formLabel" htmlFor="password">Hasło : </label>
            <input className="formInput" type="password" onChange={(e) => this.setPassword(e.target.value)}/>
            <br />
            <label className="formLabel" htmlFor="repeatPassword">Powtórz hasło : </label>
            <input className="formInput" onChange={(e) => this.setRepeatPassword(e.target.value)}/>
            <br />
            <input type="button" value="Zarejestruj" onClick={() => this.RegisterUser()} />
        </form>
    </div>
    }
    
}
