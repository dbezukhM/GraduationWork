import { Guid } from "guid-typescript";
import { IdNameModel } from "./id-name-model.model";
import { Competence } from "./competence.model";
import { ProgramResult } from "./program-result.model";

export interface EducationalProgramDetails {
    id: Guid
    name: string
    areaOfExpertise: IdNameModel
    specialization: IdNameModel
    university: IdNameModel
    faculty: IdNameModel
    educationalProgramsType: IdNameModel
    subjects: IdNameModel[]
    competences: Competence[]
    programResults: ProgramResult[]
}