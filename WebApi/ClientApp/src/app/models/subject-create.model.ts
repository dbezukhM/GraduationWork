import { Guid } from "guid-typescript";

export interface SubjectCreate {
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
    educationalProgramId: Guid
}
