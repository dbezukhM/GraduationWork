import { Guid } from "guid-typescript";
import { Competence } from "./competence.model";
import { IdNameModel } from "./id-name-model.model";
import { ProgramResult } from "./program-result.model";

export interface SubjectDetails {
    id: Guid;
    name: string;
    credits: number;
    semester: number;
    lecturesHours: number;
    seminarsHours: number;
    practicalClassesHours: number;
    laboratoryClassesHours: number;
    trainingsHours: number;
    consultationsHours: number;
    selfWorkHours: number;
    selectiveBlock: IdNameModel;
    finalControlType: IdNameModel;
    educationalProgram: IdNameModel;
    competences: Competence[];
    programResults: ProgramResult[];
}
