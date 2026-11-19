export interface Article {
    id: number;
    heading: string;
    headingImage: string | null;
    dateTimeCreated: string;
    dateTimeEdited: string | null;
    markdown: string;
    author: unknown | null;
}