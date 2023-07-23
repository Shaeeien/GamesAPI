import React from "react";
import AddGameForm from "./addgameform";
import '../layout/index.css';

export default class AddGame extends React.Component{
    constructor(props){
        super(props);
    }

    GameAdd = function(params) {
        
    }

    render(){
        return <div className="content">
                <AddGameForm />
            </div>
    }
}