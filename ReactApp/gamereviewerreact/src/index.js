import React from 'react';
import ReactDOM from 'react-dom/client';
import './layout/index.css';
import App from './App';
import { BrowserRouter } from 'react-router-dom';
import UserProvider from './user/userContext';


const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <BrowserRouter>
    <UserProvider.Provider value="">
      <App /> 
    </UserProvider.Provider>            
    </BrowserRouter>    
  </React.StrictMode>
)



