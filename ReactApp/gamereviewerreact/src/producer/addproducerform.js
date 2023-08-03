import React from 'react';
import {useState} from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

export default function AddProducerForm(){
    const navigate = useNavigate();
    const[name, setProducerName] = useState('');
    const[description, setDescription] = useState('');
    
    const HandleSubmit = async(event) => {
        event.preventDefault();
        if(name !== '' && description !== ''){
            const data = {
                'Name': name,
                'Description' : description
            }                
            
            const headers = {
                'Content-Type': 'application/json',
                'Accept' : '*/*',
                'Access-Control-Allow-Origin': '*'
              }
            console.log(data);
              const response = await axios.post(
                "https://localhost:7278/api/producers/add", JSON.stringify(data), { headers: headers }
            ).then((response) => {
                if(response.status === 200){
                    console.log("Dodano");
                }
                else{
                    console.log("Nie dodano");
                }
            });            
            
        }
    }

    return <div id="addproducerform">
        <form onSubmit={ HandleSubmit }>
            <label className="formLabel" htmlFor="name">Nazwa wydawcy : </label>
            <input className="formInput" type="text" onChange={(e) => setProducerName(e.target.value)}/>
            <br />
            <label className="formLabel" htmlFor="description">Opis : </label>
            <textarea className="formTextArea" rows="10" maxLength="400" onChange={(e) => setDescription(e.target.value)}/>
            <br />
            <input type="submit" value="Dodaj wydawcę" />
            <br />
        </form>
    </div>
}