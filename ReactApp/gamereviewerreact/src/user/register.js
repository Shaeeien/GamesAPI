import React from "react";
import RegistrationForm from "./registrationform";
import '../layout/index.css';

export default class Register extends React.Component{
    constructor(props){
        super(props);
    }

    render(){
        return <div className="content">
                <RegistrationForm />
            </div>
    }
}