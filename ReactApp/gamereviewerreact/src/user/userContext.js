import React from "react";
import { createContext } from "react";

export const UserContext = React.createContext({email : ""});
export const UserProvider = UserContext.Provider;
export const UserConsumer = UserContext.Consumer;
export default UserContext;