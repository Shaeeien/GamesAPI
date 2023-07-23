import AddProducerForm from "./addproducerform";
import React from "react";

export default class AddProducer extends React.Component{
    constructor(props){
        super(props);
    }

    render(){
        return <div className="content">
                <AddProducerForm />
            </div>
    }
}