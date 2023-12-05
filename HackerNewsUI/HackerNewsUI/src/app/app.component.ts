import { Component, VERSION } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title: string = 'HackerNewsUI';
  hackerNewsStories: HackerNewsStory[] | undefined;
  page: any;

  constructor(private http: HttpClient) {
    this.getBestStories("");
  }

  getBestStories(searchTerm: string) {
    this.http.get<HackerNewsStory[]>(
      environment.baseURL + `HackerNews?searchTerm=` + searchTerm
    )
      .subscribe({
        next: (result) => this.hackerNewsStories = result,
        error: (e) => console.error(e),
        complete: () => console.info('complete')
      });
  }
 
  search(event: KeyboardEvent) {
    this.getBestStories((event.target as HTMLTextAreaElement).value);
  }

  checkPH($event: any) {
    if ($event.currentTarget.value.indexOf("Search here") > -1)
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