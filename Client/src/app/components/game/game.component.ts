import { Component } from '@angular/core';
import { GameService } from '../../services/gameService/game.service';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent {
  constructor(
    private gameService: GameService
  ) { }

  isLoading: boolean = true;
  text: string = '';

  getText(): void {
    this.gameService.getText()
      .subscribe(res => {
        if (res.success && res.data) {
          this.text = res.data.textString;
        } else {
          // Async GET did not work, retreive default text
          this.text = this.gameService.getDefaultText();
        }

        this.isLoading = false;
      });
  }

  ngOnInit(): void {

    // This timeout is only here to show off the loading screen, it does not serve any important function

    setTimeout(() => {
      this.getText();
    }, 2000)    
  }
}
