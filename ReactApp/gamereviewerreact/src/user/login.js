import React from "react";
import LoginForm from "./loginform";
import '../layout/index.css'

export default function Login(props){    
    return <div className="content">
        <LoginForm Login={props.Login} />
    </div>
}