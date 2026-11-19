export interface EventItem {
  id: number;
  name: string;
  start: string;
  end: string | null;
  place: string;
  link: string | null;
  authorId: number;
}