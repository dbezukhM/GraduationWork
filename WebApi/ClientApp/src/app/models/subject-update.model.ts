import { Guid } from "guid-typescript";

export interface SubjectUpdate {
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
    selectiveBlockId: Guid;
    finalControlTypeId: Guid;
    competencesIds: Guid[];
    programResultsIds: Guid[];
}
