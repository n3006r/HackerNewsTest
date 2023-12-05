import { Component, VERSION } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  hackerNewsStories: HackerNewsStory[] | undefined;
  page: any;

  constructor(
    private http: HttpClient) {
    this.get("");
  }

  get(searchTerm: string) {
    this.http
      .get<HackerNewsStory[]>(
        `http://localhost:18137/HackerNews?searchTerm=` + searchTerm
      )
      .subscribe(
        result => {
          this.hackerNewsStories = result;
        },
        error => console.error(error)
      );
  }

  search(event: KeyboardEvent) {
    this.get((event.target as HTMLTextAreaElement).value);
  }

  checkPH($event: any) {
    if ($event.currentTarget.value.indexOf("Search here")>-1)
      $event.currentTarget.value = '';
  }

  checkPHOnUp($event: any) {
    if ($event.currentTarget.value == "")
      $event.currentTarget.value = 'Search here ..';
  }

  open(url: string) {
    window.open(url, "_blank");
  }
}

interface HackerNewsStory {
  title: string;
  by: string;
  url: string;
}