# 경마 게임 — 진행 상황

마지막 커밋: `216b9c5` "Feat: Unity MCP로 선택 기능 이슈(#4~#11) 씬 배선 완료" (2026-08-17). 진행 이력:
1. (2026-08-15~17) 신규 기능 7종(시작 화면, 참가 인원 조절, 동물 선택, 신호등, y축 이동, 결과 화면, 재시작) 스크립트 레이어 구현
2. (2026-08-17) 코드/에셋 개선사항 4종 반영(중복 에셋 정리, 배경 패럴랙스, 매직 넘버 노출, Find 캐싱)
3. (2026-08-17) 재미 요소 5종(동물별 능력치, 실시간 순위 HUD, 포토피니시, 부스트/장애물 존, 로컬 기록 저장) 구현 + 그중 3종은 플레이 전 온오프 토글 지원
4. (2026-08-17) Unity가 6000.5.8f1로 업그레이드됨(사용자가 새 에디터로 프로젝트를 열면서 자동 발생) + Unity MCP(CoplayDev/unity-mcp) 연결 성공
5. (2026-08-17) 컴파일을 막던 미사용 `TextMesh Pro/Examples & Extras` 폴더 제거(이후 TMP 리소스 재확인 과정에서 두 번 더 재생성되어 그때마다 삭제 + `.gitignore`에 추가)
6. (2026-08-17) **Unity MCP로 필수 배선(이슈 #1~#3) 완료 + Play 모드 전체 플로우 검증(이슈 #12)**. 검증 중 `TMP_Dropdown` 관련 실버그 발견·수정.
7. (2026-08-17) **Unity MCP로 선택 기능 이슈 #4~#11 전부 씬 배선 완료** (신호등/동물선택화면/AnimalVisual/실시간HUD/포토피니시/부스트존/온오프토글). #7(배경 패럴랙스)만 안전한 부착 대상이 없어 보류.

**현재 상태 요약**: 등록된 이슈 17개 중 **#1~#6, #8~#12(11개) 완료**, **#7만 보류**(안전한 배경 레이어 없음), **#13~#17(신규 기능/정리)은 미착수**. 씬을 Play 하면 시작 화면 → 마릿수/동물/옵션 선택 → 신호등 → 경주(부스트존·포토피니시 포함) → 결과 화면(실시간 HUD 포함) → 재시작까지 전체 기능이 배선된 상태. ([이슈 목록](https://github.com/sw0501/horse-racing-game/issues))

### 등록된 이슈

**필수 배선** — ✅ 완료 (Unity MCP로 처리, Play 모드 검증까지 끝남)
- [x] [#1 GameManager 오브젝트 생성 및 GameFlowController 필드 연결](https://github.com/sw0501/horse-racing-game/issues/1) — closed
- [x] [#2 EndMenu 구성 (결과 화면 + 처음으로 버튼)](https://github.com/sw0501/horse-racing-game/issues/2) — closed
- [x] [#3 NextButton에 OnHorseCountConfirmed 연결](https://github.com/sw0501/horse-racing-game/issues/3) — closed

**기능별 에디터 배치** — #7 제외 전부 완료
- [x] [#4 출발 신호등 UI 제작](https://github.com/sw0501/horse-racing-game/issues/4) — closed. 3색 UI Image 생성·연결, 첫 점등까지 Play 확인. 코루틴 타이머 실완주는 에디터 언포커스(macOS App Nap)로 이 세션에서 재현 못함.
- [x] [#5 ChooseName 동물 선택 화면 구성](https://github.com/sw0501/horse-racing-game/issues/5) — closed. 버튼 5개(Fox/Rabbit 제외) 생성·연결, Play 모드에서 Boar 선택 시 Animator·스탯 반영까지 확인.
- [x] [#6 말 오브젝트에 AnimalVisual 부착 및 Controller 연결](https://github.com/sw0501/horse-racing-game/issues/6) — closed.
- [ ] [#7 배경 패럴랙스 레이어 배치](https://github.com/sw0501/horse-racing-game/issues/7) — **보류**. `Map`이 배경 스프라이트가 아니라 트랙 데코 전체(자식 ~350개)를 담은 컨테이너라 패럴랙스를 붙이면 시각적으로 깨짐. 전용 원경 레이어 아트가 있어야 진행 가능.
- [x] [#8 실시간 순위 HUD 배치](https://github.com/sw0501/horse-racing-game/issues/8) — closed.
- [x] [#9 포토피니시 연출 오브젝트 배치](https://github.com/sw0501/horse-racing-game/issues/9) — closed.
- [x] [#10 부스트/장애물 존 배치](https://github.com/sw0501/horse-racing-game/issues/10) — closed. `HosreDieOn`과 동일한 트리거 패턴으로 트랙 중간(x=60)에 배치.
- [x] [#11 게임 옵션 온오프 토글 UI 배치](https://github.com/sw0501/horse-racing-game/issues/11) — closed.

**QA** — ✅ 완료
- [x] [#12 전체 플로우 동작 확인](https://github.com/sw0501/horse-racing-game/issues/12) — closed. Play 모드에서 시작화면→3마리 선택→Next→동물확정→4.5초 대기→경주→완주(1,2,3등 순위 텍스트)→로컬 기록 저장→EndMenu→처음으로→StartMenu 복귀까지 실제로 확인함.

**신규 기능** — 미착수
- [ ] [#13 베팅(예측) 시스템 추가](https://github.com/sw0501/horse-racing-game/issues/13)
- [ ] [#14 효과음 강화](https://github.com/sw0501/horse-racing-game/issues/14)
- [ ] [#15 날씨/트랙 컨디션 랜덤 이벤트](https://github.com/sw0501/horse-racing-game/issues/15)

**정리** — 미착수
- [ ] [#16 스텁 스크립트(HorseRemoveLine, test) 정리](https://github.com/sw0501/horse-racing-game/issues/16)
- [ ] [#17 레포/빌드 관리 프로세스 정리](https://github.com/sw0501/horse-racing-game/issues/17)

## 완료된 기능 (기존)

README에 명시된 3가지 핵심 기능:

- [x] **말의 개별 속도 랜덤 지정** — `Horse_Move.cs`: 매 `FixedUpdate`마다 `Random.Range(0.02f, 0.2f)`로 이동 속도를 새로 굴려 각 말이 독립적으로 전진.
- [x] **카메라 출발 및 멈춤 기능** — `CamerMoveStart.cs` / `CameraMoveEnd.cs` 트리거가 `CameraMove.cs`의 활성/비활성을 토글.
- [x] **도착 순위 판별 기능** — `HorseOff.cs`: 결승선 트리거에서 태그(`"1"`~`"5"`)로 말을 구분해 도착 순서 카운터를 `TMP_Text`에 기록.

추가로 커밋 로그 기준 구현된 것:

- [x] **낙마(fall) 기능** — `Horse_Move.cs`의 `LiveOrDie` 로직 + `HosreDieOn.cs`.
- [x] **출발 대기 연출** — `wait.cs`(현재는 아래 로드맵 반영으로 자동 실행에서 수동 트리거 방식으로 변경됨).
- [x] **빌드 산출물** — `build.zip`이 저장소에 커밋되어 있음.
- [x] **한글 주석 인코딩 복구** — `Horse_Move.cs`가 CP949로 저장돼 있던 것을 UTF-8로 재저장.

## 미완성 / 스텁 상태 코드

- [x] `Assets/script/Backgorund_Move.cs` — 패럴랙스 스크롤 스크립트로 구현 완료(아래 "개선하면 좋을 사항" 참고). 씬의 배경 오브젝트에 붙이는 건 에디터 작업 필요.
- [ ] `Assets/HorseRemoveLine.cs` — 이름상 완주 후 말 제거/정리 기능으로 추정되나 로직 없음.
- [ ] `Assets/script/test.cs` — 테스트용 빈 스크립트. 정리 또는 삭제 검토.

## 로드맵 — 신규 기능 7종 구현 현황

**스크립트(C#) 레이어는 전부 구현 완료.** 단, 이 환경은 Unity 에디터 GUI를 조작할 수 없어 씬(.unity)의 새 UI 생성·Inspector 참조 연결·프리팹 작업은 하지 못함. 아래 "에디터 작업 체크리스트"를 사람이 직접 완료해야 실제로 동작함(그 전까지는 기능 미완료로 간주).

씬 조사 결과, `Canvas` 하위에 이미 만들어져 있었지만 비활성/미연결 상태였던 UI를 최대한 재사용함:
- `StartMenu`(비활성) — `SelectHorseNum` 드롭다운("2 Horse"~"5 Horse") + `NextButton`
- `ChooseName`(비활성, 자식 없는 빈 껍데기) — 동물 선택 화면 예정 위치
- `Ranking`(활성) — 순위 `TMP_Text` 5개, `HorseOff`가 이미 참조
- `EndMenu`(비활성, 배경 스프라이트만 존재, 버튼 없음)

### 1. 게임 시작 화면
- [x] 스크립트 구현 — `GameFlowController.cs`(`Awake()`에서 `StartMenu` 활성화, `ChooseName`/`EndMenu` 비활성화, `RaceConfig` 초기화) + `wait.cs`(자동 실행 제거, `BeginWait()` 공개 메서드로 전환).
- [x] 에디터 연결 — Unity MCP로 `GameManager` 생성 + 필드 연결, `NextButton.onClick` 배선 완료(이슈 #1, #3). Play 모드 실동작 검증 완료.

### 2. 참가 플레이어 수 조절 기능
- [x] 스크립트 구현 — `GameFlowController.OnHorseCountConfirmed()`, `CameraMove.cs`(하드코딩 5마리 제거, `RaceConfig.HorseCount` 기반 순회로 재작성), `HorseOff.cs`(완주 인원 도달 시 `OnRaceFinished()` 호출), `Select_Horse_Number.cs`(드롭다운 → `RaceConfig` 반영).
- [x] 에디터 연결 — 완료(이슈 #1, #3). **버그 발견·수정**: 실제 드롭다운 컴포넌트가 `UnityEngine.UI.Dropdown`이 아니라 `TMPro.TMP_Dropdown`이었음 — 원래 코드대로였으면 NextButton 클릭 시 NullReferenceException 발생. `GameFlowController.cs`/`Select_Horse_Number.cs` 모두 수정. Play 모드에서 3마리로 설정 → 정상 반영 확인.

### 3. 플레이어 동물 선택 기능
- [x] 스크립트 구현 — `RaceConfig.cs`(`AnimalType` enum), `AnimalVisual.cs`(Animator Controller 교체), `AnimalSelectController.cs`(버튼 핸들러).
- [x] **범위 축소 결정**: 레인별 개별 동물이 아니라 참가자 전원이 같은 동물 종으로 통일되도록 구현(레인별 선택은 UI 복잡도가 크게 늘어나 이번 범위에서 제외, 필요시 추후 확장).
- [x] 에디터 연결 — 완료(이슈 #5, #6). `ChooseName`에 버튼 5개(Horse/Bear/Boar/Deer/Wolf) 생성, `horse_1`~`5`에 `AnimalVisual` 부착 및 컨트롤러 매핑. Play 모드에서 Boar 선택 시 Animator·스탯 반영까지 확인.
- [ ] Fox/Rabbit용 Animator Controller 애셋 제작 — 여전히 미해결(원본 스프라이트가 슬라이스 안 된 정지 이미지라 아트 리소스 추가 필요, 코드로 대체 불가).

### 4. 출발 직전 신호등 UI
- [x] 스크립트 구현 — `StartLightController.cs`(순차 점등 후 `wait.BeginWait(0f)` 호출, 자체 지연이 `wait.cs`의 기본 4.5초를 대체).
- [x] 에디터 연결 — 완료(이슈 #4). 빨강/노랑/초록 UI Image 3개(별도 스프라이트 없이 색상만 사용) 생성 및 연결. Play 모드에서 점등 시작까지 확인(코루틴 실완주는 에디터 언포커스 이슈로 이 세션에서 끝까지 재현은 못함).

### 5. 동물 움직임 y축 추가
- [x] 스크립트 구현 완료 — `Horse_Move.cs`: 시작 y좌표(`startY`) 기준 `verticalWobbleRange`(기본 0.3) 내에서 매 프레임 `verticalWobbleSpeed`(기본 0.05)만큼 랜덤 누적·클램프. x축 전진 로직은 유지. **에디터 작업 불필요, 바로 동작.**

### 6. 게임 종료 후 순위 보여주는 화면
- [x] 스크립트 구현 — `EndMenuController.cs`(`ShowResults()`), `HorseOff.cs` → `GameFlowController.OnRaceFinished()` → `EndMenuController.ShowResults()` 연쇄 호출.
- [x] 에디터 작업 — Unity MCP로 `EndMenuController` 부착 + `RecordSummaryText` 생성/연결 완료(이슈 #2). 최종 순위는 `Ranking`(Canvas 자식 순서상 `EndMenu`보다 위에 렌더링됨)이 그대로 보여주는 방식으로 처리 — 별도 텍스트 복제 불필요했음. Play 모드에서 완주 후 `EndMenu` 자동 노출 + 순위 텍스트 정상 확인.

### 7. 게임 시작 화면으로 돌아가는 기능
- [x] 스크립트 구현 완료 — `EndMenuController.ReturnToStart()`가 `SceneManager.LoadScene(현재 씬)`으로 전체 상태를 재로드 방식으로 초기화(필드 단위 수동 리셋보다 안전하다고 판단해 이 방식 채택). 씬이 하나뿐이라 별도 시작 화면 씬 불필요.
- [x] 에디터 작업 — Unity MCP로 `ReturnToStartButton` 생성 + `EndMenuController.ReturnToStart` 연결 완료(이슈 #2). Play 모드에서 클릭 시 씬 재로드 및 `StartMenu` 재노출 확인.

## 에디터 작업 체크리스트 (Unity에서 사람이 직접 해야 함)

**전부 완료. 8번(배경 패럴랙스)만 안전한 부착 대상이 없어 보류.** Unity MCP(CoplayDev/unity-mcp)로 1~7, 9~12번 전부 처리함.

- [x] **1. GameManager 오브젝트 생성** — 완료(이슈 #1). `GameManager`에 `GameFlowController` 부착. 모든 필드(`startMenu`/`chooseAnimalMenu`/`endMenu`/`horseNumberSelector`/`waitScript`/`endMenuController`/`startLight`/`liveRanking`/토글 3종) 연결 완료.

- [x] **2. EndMenu 구성** — 완료(이슈 #2). `EndMenuController` 부착, `ReturnToStartButton`("처음으로" 버튼) + `RecordSummaryText` 생성 및 연결, `HorseOff.gameFlow` 연결까지 끝남. 최종 순위는 `Ranking`이 `EndMenu`보다 Canvas 자식 순서상 위라 별도 텍스트 복제 없이 그대로 보임.

- [x] **3. 신호등 UI** — 완료(이슈 #4). `StartLightPanel` 아래 빨강/노랑/초록 UI Image 3개(색상만 사용, 별도 스프라이트 불필요) 생성, `StartLightController.lights`/`waitScript`, `GameFlowController.startLight` 연결. Play 모드에서 점등 시작 확인.

- [x] **4. NextButton 추가 연결** — 완료(이슈 #3). `execute_code`로 `UnityEventTools.AddPersistentListener` 사용해 기존 호출(`StartMenu.SetActive(false)`, `ChooseName.SetActive`) 유지한 채 `GameFlowController.OnHorseCountConfirmed` 추가.

- [x] **5. ChooseName(동물 선택) 화면 구성** — 완료(이슈 #5). 버튼 5개(말/곰/멧돼지/사슴/늑대, Fox·Rabbit 제외) 생성, `AnimalSelectController` 부착·연결, 각 버튼에 `SelectAnimal(int)` 연결. **주의**: `AnimalType` enum이 `Horse=0,Bear=1,Boar=2,Deer=3,Fox=4,Wolf=5,Rabbit=6`이라 Fox(4)를 건너뛰므로 늑대 버튼은 반드시 `5`로 연결해야 함 — 처음에 순차 인덱스(0~4)로 잘못 연결해서 늑대가 Fox를 가리키는 버그가 있었고 발견 즉시 수정함.

- [x] **6. 말 오브젝트에 AnimalVisual 부착** — 완료(이슈 #6). `horse_1`~`5`에 `AnimalVisual` 부착, `controllers` 배열에 5종 컨트롤러 매핑:
    - Horse → `Assets/horse/GameObject.controller`
    - Bear → `Assets/Wild Animals/Bear/Bear_Run.controller`
    - Boar → `Assets/Wild Animals/Boar/Boar_Run.controller` (처음엔 `Square.controller`만 있는 줄 알았는데, 정식 명명된 `Boar_Run.controller`가 따로 있었음)
    - Deer → `Assets/Wild Animals/Deer/Deer.controller`
    - Wolf → `Assets/Wild Animals/Wolf/Wolf.controller`
    - Fox, Rabbit → **여전히 컨트롤러 애셋 없음.** `Fox_Run.png` 등이 `spriteMode: 1`(Single, 슬라이스 안 된 정지 이미지 1장)이라 애니메이션 클립을 만들 재료가 없음 — 아트 리소스가 추가로 필요한 코드로 대체 불가능한 작업이라 5종만 제공하기로 함.

- [x] **7. 동작 확인** — 완료(이슈 #12). Unity MCP `execute_code`로 Dropdown 값 변경 + 버튼 클릭을 코드로 시뮬레이션해 전체 플로우 1회 통관 확인. Boar 동물 선택 시 Animator·스탯이 정확히 바뀌는 것도 별도로 재확인함(이슈 #5).

- [ ] **8. 배경 패럴랙스(선택 사항, 보류)**
  - 조사 결과 `Map` 오브젝트가 배경 스프라이트 하나가 아니라 트랙 데코 전체(자식 약 350개)를 담은 컨테이너였음 — 여기 `Backgorund_Move`를 붙이면 트랙 전체가 카메라와 별도로 움직여 시각적으로 깨짐.
  - 진행하려면 카메라보다 뒤쪽(낮은 z-order/원경)에 배치할 **전용 배경 이미지 애셋**이 먼저 필요함. 애셋이 생기면 그 오브젝트에 `Backgorund_Move` 부착 + `parallaxFactor`(0~1, 낮을수록 더 멀리 있는 느낌) 설정.

- [x] **9. 실시간 순위 HUD 배치** — 완료(이슈 #8). `Canvas` 하위에 `LiveRankingHUD`(TMP_Text, 화면 좌상단) 생성, `LiveRankingController.liveRankingText`/`GameFlowController.liveRanking` 연결.

- [x] **10. 포토피니시 연출 배치** — 완료(이슈 #9). `PhotoFinish` 오브젝트 생성, `PhotoFinishController` 부착, `HorseOff.photoFinish` 연결.

- [x] **11. 부스트/장애물 존 배치** — 완료(이슈 #10). `HosreDieOn`(`Die`)과 동일한 패턴(Rigidbody2D Dynamic + gravityScale 0, BoxCollider2D isTrigger 크기 1×15)으로 `BoostZone` 오브젝트를 트랙 중간(x=60, 출발 x=-9.2와 낙마구간 x=130 사이)에 배치, `speedMultiplier=1.6`/`durationSeconds=1`로 설정.

- [x] **12. 게임 시작 전 기능 온오프 토글 배치** — 완료(이슈 #11). `StartMenu`에 Toggle 3개("동물별 능력치"/"부스트·장애물 존"/"기록 저장", 기본 체크됨) 생성, `GameFlowController.useAnimalStatsToggle`/`useBoostZonesToggle`/`saveLocalRecordsToggle`에 연결.

## 개선하면 좋을 사항

- [x] **중복 에셋 정리** — GUID로 실제 씬 참조 여부를 확인해 미사용 사본 삭제: `Assets/Scenes/Mario Kart Race Start Sound Effect.mp3`(씬은 `Assets/`의 것만 참조), `Assets/script/하츠네미쿠_-_폿핏포_.mp3`(내용도 다른 별도 편집본, 씬은 `Assets/`의 `(mp3cut.net)` 버전만 참조), 루트 `map.jpg`(Assets 밖이라 Unity 애셋도 아니고 README도 미참조) 삭제.
- [x] **`Backgorund_Move.cs` 구현** — 카메라 이동량의 일부(`parallaxFactor`, 기본 0.5)만큼만 따라 움직이는 범용 패럴랙스 스크립트로 구현. 기존 `Background` 타일 18개의 배치는 건드리지 않았고(위치를 검증 없이 옮기면 트랙과 어긋날 위험이 있어 그대로 둠), 별도 배경 레이어에 붙이는 용도로 사용 가능. **에디터 작업 필요**: 붙일 대상 오브젝트에 컴포넌트 추가 + `parallaxFactor` 튜닝.
- [x] **매직 넘버 Inspector 노출** — `Horse_Move.cs`에 `fallCheckRange`(기본 50000)/`fallCheckTarget`(기본 44)/`minSpeed`/`maxSpeed` 필드 추가, `wait.cs`의 `startDelaySeconds`는 앞서 기능 구현 때 이미 필드로 노출됨.
- [x] **`GameObject.Find` 매 프레임 호출 캐싱** — `CameraMove.cs`가 `Start()`에서 `horse_1`~`horse_5` 참조를 1회만 `Find`로 캐싱하고, 이후 `FixedUpdate`는 캐시된 배열을 `RaceConfig.HorseCount`만큼만 순회하도록 변경.
- [x] **Fox/Rabbit Animator Controller 관련 확인** — 원인 파악 완료: `Fox_Run.png` 등이 `spriteMode: 1`(Single, 슬라이스 안 된 정지 이미지 1장)이라 애니메이션 클립 자체를 만들 수 없는 상태(코드로 대체 불가한 아트 리소스 부족). 체크리스트 6번에 대안(5종만 제공 또는 정지 이미지용 단일 상태 컨트롤러) 추가.

## 넣으면 재밌을 기능

동물별 능력치/실시간 HUD/포토피니시/부스트존/로컬 기록은 구현 완료. 남은 후보 중 **베팅 시스템**이 이미 구현된 동물별 능력치 차별화와 바로 시너지가 나서(내가 고른 동물의 스탯을 보고 우승마를 예측) 우선순위로 추천.

- [ ] **베팅(예측) 시스템** — 레이스 시작 전 "이번 판 우승마"를 하나 찍게 하고, 결과 화면에서 적중 여부를 보여줌. 가상 포인트 + 배당률을 추가하면 더 재밌어짐.
- [x] **동물별 능력치 차별화** — 신규 `AnimalStats.cs`: 종별로 `minSpeed`/`maxSpeed`/낙마 확률 배율(`fallRiskMultiplier`) 프로필을 정의(예: 곰=느리지만 낙마 확률 0.5배, 토끼=빠르지만 1.6배). `GameFlowController.ApplySelectedAnimal()`이 동물 선택 확정 시 해당 프로필을 `Horse_Move`에 적용. `RaceConfig.UseAnimalStats` 토글로 온오프 가능(꺼져 있으면 기존처럼 전종 동일 스탯).
- [x] **실시간 순위 HUD** — 신규 `LiveRankingController.cs`: `horse_1`~`horse_5` 참조를 `CameraMove`와 동일한 방식으로 1회 캐싱한 뒤 `updateIntervalSeconds`(기본 0.2초)마다 x좌표 기준으로 정렬해 TMP_Text 1개에 "1위: N번" 줄바꿈 목록으로 갱신. `GameFlowController.OnRaceFinished()`에서 `liveRanking.enabled = false`로 자동 정지. 에디터 배치 완료(이슈 #8).
- [x] **접전 연출(포토피니시)** — 신규 `PhotoFinishController.cs`(카메라 `orthographicSize`를 줌인 → `Time.timeScale`을 낮춰 슬로우모션(실시간 `WaitForSecondsRealtime`으로 지속시간 보장) → 줌아웃 순서로 재생). `HorseOff.cs`가 1위 완주 시각을 기록해두고, 2위가 `photoFinishThresholdSeconds`(기본 0.3초) 이내에 들어오면 자동 트리거. 에디터 배치 완료(이슈 #9).
- [x] **부스트/장애물 존** — `Horse_Move.cs`에 `ApplySpeedMultiplier(multiplier, duration)` 추가(1보다 크면 부스트, 작으면 감속, 시간 지나면 자동 원복). 신규 `BoostZone.cs`가 `HosreDieOn`과 동일한 트리거 패턴으로 진입한 말에게 배율 적용. `RaceConfig.UseBoostZones` 토글로 온오프 가능(꺼져 있으면 존에 들어와도 아무 효과 없음). 에디터 배치 완료(이슈 #10, 트랙 x=60 지점).
- [ ] **효과음 강화** — 낙마 효과음, 결승선 통과 순위별 팡파레, 신호등 전환음(`StartLightController`에 붙이기 좋음) 추가.
- [x] **로컬 기록 저장** — 신규 `LocalRecordManager.cs`(`PlayerPrefs` 기반): 레인 번호별 누적 우승 횟수, 총 플레이 판 수, 최고 완주 기록(초)을 저장. `wait.cs`가 말이 실제로 출발하는 시점을 `RaceConfig.RaceStartTime`에 기록해두고, `HorseOff.cs`가 1위 완주 시 해당 레인·소요시간을 저장. `EndMenuController.recordSummaryText`(선택 필드)에 연결하면 결과 화면에 "누적 N판 플레이 / 최고 기록 M초" 요약 표시. `RaceConfig.SaveLocalRecords` 토글로 온오프 가능.
- [ ] **날씨/트랙 컨디션 랜덤 이벤트** — 판마다 "비 오는 날(속도 -20%)", "맑은 날(낙마 확률 감소)" 등을 시작 화면에 랜덤 노출해 매판 다른 느낌 부여.

## 알려진 이슈 / 정리 필요 사항

- [ ] 자동화된 빌드/테스트/CI 파이프라인 없음 — Unity Editor(현재 6000.5.8f1 — 2026-08-17에 사용자가 새 에디터로 프로젝트를 열며 2021.3.22f1에서 자동 업그레이드됨)에서 수동 실행/빌드만 가능.
- [ ] `build.zip`처럼 바이너리 빌드 산출물을 저장소에 직접 커밋하는 방식 유지 여부 검토(용량 이슈).
- [ ] 스텁 스크립트(`HorseRemoveLine`, `test`) 실제 구현 여부 결정 필요.
- [ ] Fox/Rabbit용 Animator Controller가 없어 동물 선택 목록에 아직 넣을 수 없음(원본 스프라이트가 슬라이스 안 된 정지 이미지 1장뿐이라 아트 리소스 추가 필요).
- [ ] `Assets/TextMesh Pro/Examples & Extras/`가 TMP 리소스 재확인 시 자동 재생성됨(우리 게임과 무관, `.gitignore`로 git 추적은 막아뒀지만 로컬 디스크에는 계속 생길 수 있음).
- [ ] 배경 패럴랙스(#7)용 전용 원경 배경 아트 애셋 필요 — 없으면 진행 불가.
