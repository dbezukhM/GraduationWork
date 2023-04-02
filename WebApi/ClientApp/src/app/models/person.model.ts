import { Guid } from "guid-typescript";
import { IdNameModel } from "./id-name-model.model";

export interface Person {
    id: Guid;
    firstName: string;
    lastName: string;
    email: string;
    isFirstPasswordChanged: boolean;
    isAdmin: boolean;
    workingProgramsAuthor: IdNameModel[];
    workingProgramsApprover: IdNameModel[];
    fullName: string;
}
