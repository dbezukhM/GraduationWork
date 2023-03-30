import { Guid } from "guid-typescript";
import { IdNameModel } from "./id-name-model.model";

export interface WorkingProgramDetails {
    id: Guid
    name: string
    subject: IdNameModel
    educationalProgram: IdNameModel
    createdByName: string
    approvedByName: string
    isAvailable: boolean
}
